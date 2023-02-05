const NetgearRouter = require("../router");
const { version } = require("../package.json");
const os = require("os");

class NetgearQuery {
  router = null;
  options = {};
  log = [];
  errorCount = 0;
  t0 = Date.now();
  shorttest = false;
  session = {
    nodeVersion: "",
    netgearPackageVersion: "",
    os: "",
    info: {},
    timeTaken: 0,
  };
  attachedDevices = {
    devices: {
      count: 0,
      list: [],
    },
    router: {},
    timeTaken: 0,
  };
  allowOrBlockDevices = {
    successful: false,
    router: {},
    timeTaken: 0,
  };
  error = {
    hasError: false,
    count: 0,
    message: "",
    stack: {},
    lastResponse: "",
    router: {},
  };
  errors = [];
  routerInfo = {
    hostInfo: {},
    autoLogin: {
      count: 0,
      methodeOneSuccess: false,
      methodeTwoSuccess: false,
      timeTaken: 0,
    },
    router: {},
    systemInfo: {},
    supportFeatures: {},
    lanConfig: {},
    wanConfig: {},
    traffic: {},
  };
  routerSpeed = {
    timeTaken: 0,
  };

  constructor(opts) {
    this.options = opts || {};
    this.router = new NetgearRouter();
    this.t0 = Date.now();
    this.clearDataObjects();
    this.setupSession();
  }

  async clearDataObjects() {
    this.clearSessionObject();
    this.clearAttachedDevicesObject();
    this.clearAllowOrBlockDevicesObject();
    this.clearErrorObject();
    this.clearRouterSpeedObject();
    this.errors = [];
  }

  clearSessionObject() {
    this.session = {
      nodeVersion: "",
      netgearPackageVersion: "",
      os: "",
      info: {},
      timeTaken: 0,
    };
  }

  clearAttachedDevicesObject() {
    this.attachedDevices = {
      devices: {
        count: 0,
        list: [],
      },
      timeTaken: 0,
    };
  }

  clearAllowOrBlockDevicesObject() {
    this.allowOrBlockDevices = {
      successful: false,
      timeTaken: 0,
    };
  }

  clearErrorObject() {
    this.error = {
      hasError: false,
      count: 0,
      message: "",
      stack: {},
      lastResponse: "",
      router: {},
    };
  }

  clearRouterSpeedObject() {
    this.routerSpeed = {
      timeTaken: 0,
    };
  }

  async setupSession() {
    try {
      this.session.nodeVersion = process.version;
      this.session.netgearPackageVersion = version;
      this.session.os = `${os.platform()} ${os.release()}`;

      Object.keys(this.options).forEach((opt) => {
        if (opt === "info") {
          this.session.info[opt] = this.options[opt];
          return;
        }
        if (opt === "shorttest") {
          if (this.options.shorttest) {
            this.shorttest = true;
            this.router.timeout = 5000;
          }
          return;
        }

        this.router[opt] = this.options[opt];
      });

      this.session.timeTaken = this.getTime();
    } catch (error) {
      return this.setError(error);
    }
  }

  async speedTest() {
    await this.setupSession();
    await this.router.login();
    try {
      const speed = await this.router.speedTest();
      this.routerSpeed.timeTaken = this.getTime();

      return Object.assign(this.routerSpeed, speed);
    } catch (error) {
      return await this.setError(error);
    }
  }

  discover = async () => {
    try {
      return Promise.resolve(this.router.discover());
    } catch (error) {
      return Promise.reject(error);
    }
  };

  async getDevicesAttached() {
    try {
      this.t0 = Date.now();

      this.clearAttachedDevicesObject();

      await this.router.login();

      let attachedDevices = await this.router._getAttachedDevices();

      const attachedDevices2 = await this.router
        ._getAttachedDevices2()
        .catch((error) => this.logError(error));

      const devices = (parent, child) => {
        const combined = parent;
        let added = false;

        for (let j = 0; j < child.length; j++) {
          added = combined.findIndex((el) => {
            return el.MAC === child[j].MAC;
          });

          if (added === -1) {
            combined.push(child[j]);
          }
        }
      

        return combined;
      };

      this.attachedDevices.devices.list = devices(
        attachedDevices2,
        attachedDevices
      );

      this.attachedDevices.devices.count =
        this.attachedDevices.devices.list.length;
      this.attachedDevices.devices.timeTaken = this.getTime();

      return this.attachedDevices;
    } catch (error) {
      return await this.setError(error);
    }
  }

  async allowOrBlockDevice(payload) {
    try {
      this.t0 = Date.now();

      this.clearAllowOrBlockDevicesObject();
      await this.router.login();
      await this.router.setBlockDeviceEnable(true);
      await this.router.setBlockDevice(payload.mac, payload.action);

      this.allowOrBlockDevices.successful = true;

      return this.allowOrBlockDevices;
    } catch (error) {
      return await this.setError(error);
    }
  }

  async logError(error) {
    errorCount += 1;
    this.error.hasError = true;
    this.error.count = errorCount;
    if (
      error.message ===
      "404 Not Found. The requested function/page is not available"
    ) {
      return {};
    }
    this.error.message = error.message;
    this.error.lastResponse = this.router.lastResponse;

    if (!this.router.loggedIn) {
      await this.router.login();
    }
    return {};
  }

  getTime() {
    let timeTaken = (Date.now() - this.t0) / 1000;

    return timeTaken;
  }

  async pushToLog() {}

  async setError(error) {
    this.error.count += 1;
    this.router.password = "*****";
    this.router.sessionId = "*****";
    this.router.username = "*****";
    this.error.hasError = true;
    this.error.message = error.message;
    this.error.stack = error.stack;
    this.error.router = this.router;
    this.errors.push(this.error);
    return this.errors;
  }

  routerInfo = {
    info: {},
    timeTaken: 0,
  };

  async getRouterBasicInfo() {
    try {
      this.t0 = Date.now();
      this.routerInfo.info = await this.router.getCurrentSetting();
      this.routerInfo.timeTaken = this.getTime();
      return this.routerInfo;
    } catch (error) {
      return await this.setError(error);
    }
  }

  async getRouterHostInfo() {
    try {
      this.t0 = Date.now();
      this.routerInfo.info = await this.router._discoverAllHostsInfo();
      this.routerInfo.timeTaken = this.getTime();
      return this.routerInfo;
    } catch (error) {
      return await this.setError(error);
    }
  }

  async getRouterVersionInfo() {
    try {
      this.t0 = Date.now();
      let info = (await this.router.getInfo()) || {};
      info.SerialNumber = "**********";

      this.routerInfo.info = info;
      this.routerInfo.timeTaken = this.getTime();
      return this.routerInfo;
    } catch (error) {
      return await this.setError(error);
    }
  }

  autologinInfo = {
    method: 0,
    methodeOneSuccess: false,
    methodeTwoSuccess: false,
    timeTaken: 0,
  };

  async getAutologinInfo() {
    try {
      this.t0 = Date.now();

      await this.router.login();

      await this.router
        .login({ method: 1 })
        .then(() => (this.autologinInfo.methodeOneSuccess = true))
        .catch((error) => (this.autologinInfo.methodeOneSuccess = false));

      await this.router
        .login({ method: 2 })
        .then(() => (this.autologinInfo.methodeTwoSuccess = true))
        .catch((error) => (this.autologinInfo.methodeTwoSuccess = false));

      await this.router.login();

      this.autologinInfo.method = this.router.loginMethod;
      this.autologinInfo.timeTaken = this.getTime();

      return this.autologinInfo;
    } catch (error) {
      return await this.setError(error);
    }
  }

  async getRouterInfo() {}
}

module.exports = NetgearQuery;

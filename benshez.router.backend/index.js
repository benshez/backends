const express = require("express");
const cors = require("cors");
const NetgearQuery = require("./query");
const config = require("dotenv").config();
const router = express.Router();
const app = express();

app
  .options(
    "*",
    cors({ origin: config.parsed.AllowedOrigins, optionsSuccessStatus: 200 })
  )
  .use(
    cors({ origin: config.parsed.AllowedOrigins, optionsSuccessStatus: 200 }),
    express.json()
  );

const port = config.parsed.AppPort;
const host = config.parsed.AppHost;
const options = {
  password: config.parsed.RouterPassword,
  username: config.parsed.RouterUser,
  host: config.parsed.RouterHost,
  port: null,
};

app.get("/discover", async (req, res) => {
  try {
    const router = new NetgearQuery(options);
    const data = await router.discover();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}`);
  }
});

app.get("/attachedDevices", async (req, res) => {
  try {
    const router = new NetgearQuery(options);
    const data = await router.getDevicesAttached();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}`);
  }
});

app.get("/routerBasicInfo", async (req, res) => {
  try {
    await router.setupSession();
    const data = await router.getRouterBasicInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/routerHostInfo", async (req, res) => {
  try {
    await router.setupSession();
    const data = await router.getRouterHostInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/autologinInfo", async (req, res) => {
  try {
    const router = new NetgearQuery(options);
    const data = await router.getAutologinInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/routerVersionInfo", async (req, res) => {
  try {
    await router.setupSession();
    const data = await router.getRouterVersionInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/speedTest", async (req, res) => {
  try {
    req.setTimeout(120000);
    const router = new NetgearQuery(options);
    const data = await router.speedTest();
    res.send(data);
  } catch (error) {
    res.send(
      `${JSON.stringify(req.query)} - error ${JSON.stringify(error.stack)}.`
    );
  }
});

app.post("/allowOrBlockDevice", async (req, res) => {
  try {
    const payload = req.body;
    data = await router.allowOrBlockDevice(payload);
    res.send(data);
  } catch (error) {
    res.send(`${payload} - error ${e.message}.`);
  }
});

app.listen(port, host, () => {
  console.log(`benshez.router app listening on http://${host}:${port}`);
});

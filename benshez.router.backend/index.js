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
    const query = new NetgearQuery(options);
    const data = await query.discover();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}`);
  }
});

app.get("/attachedDevices", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.getDevicesAttached();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}`);
  }
});

app.get("/routerBasicInfo", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.getRouterBasicInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/routerHostInfo", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.getRouterHostInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/autologinInfo", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.getAutologinInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/routerVersionInfo", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.getRouterVersionInfo();
    res.send(data);
  } catch (error) {
    res.send(`${req.query} - error ${error.message}.`);
  }
});

app.get("/speedTest", async (req, res) => {
  try {
    const query = new NetgearQuery(options);
    const data = await query.speedTest();
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
    const query = new NetgearQuery(options);
    data = await query.allowOrBlockDevice(payload);
    res.send(data);
  } catch (error) {
    res.send(`${payload} - error ${e.message}.`);
  }
});

app.listen(port, host, () => {
  console.log(`benshez.router app listening on http://${host}:${port}`);
});

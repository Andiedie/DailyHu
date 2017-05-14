const service = require('../service');
const assert = require('assert');

// API：获取数据源的元数据，如LOGO等
module.exports = async ctx => {
  try {
    ctx.body = await service.proxy.getMetadata();
  } catch (err) {
    ctx.status = 400;
    ctx.body = err.message;
    console.error(err);
  }
};

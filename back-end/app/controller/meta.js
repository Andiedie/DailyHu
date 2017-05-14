const service = require('../service');
const assert = require('assert');

module.exports = async ctx => {
  try {
    ctx.body = await service.proxy.getMetadata();
  } catch (err) {
    ctx.status = 400;
    ctx.body = err.message;
    console.error(err);
  }
};

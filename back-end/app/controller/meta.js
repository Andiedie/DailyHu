const service = require('../service');
const assert = require('assert');

module.exports = async ctx => {
  try {
    const {site} = ctx.query;
    assert(site, 'site name needed');

    ctx.body = await service.proxy.getMetadata(site);
  } catch (err) {
    ctx.status = 400;
    ctx.body = err.message;
  }
};

const service = require('../service');
const assert = require('assert');

// API：获取数据源的目录
module.exports = async ctx => {
  const {site, page} = ctx.query;

  try {
    assert(site, 'site name needed.');
    assert(page, 'positive page number needed.');

    ctx.body = await service.proxy.getList(site, page);
  } catch (err) {
    ctx.status = 400;
    ctx.body = err.message;
    console.error(err);
  }
};

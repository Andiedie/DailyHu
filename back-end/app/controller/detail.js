const service = require('../service');
const assert = require('assert');

// API接口：获取文章详情
module.exports = async ctx => {
  try {
    const {url} = ctx.query;
    assert(url, 'url needed');

    ctx.body = await service.proxy.getDetail(url);
  } catch (err) {
    ctx.status = 400;
    ctx.body = err.message;
    console.error(err);
  }
};

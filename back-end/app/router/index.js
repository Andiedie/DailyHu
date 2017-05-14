const Router = require('koa-router');
const controllers = require('../controller');
const router = new Router();

// 三个API路由到对应的controller
router
  .get('/list', controllers.list)
  .get('/detail', controllers.detail)
  .get('/meta', controllers.meta);

module.exports = router;

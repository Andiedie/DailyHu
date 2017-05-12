const Router = require('koa-router');
const controllers = require('../controller');
const router = new Router();

router
  .get('/list', controllers.list)
  .get('/detail', controllers.detail)
  .get('/meta', controllers.meta);

module.exports = router;

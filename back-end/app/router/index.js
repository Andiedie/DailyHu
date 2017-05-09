const Router = require('koa-router');
const controllers = require('../controller');
const router = new Router();

for (const [name, control] of Object.entries(controllers)) {
  router.get(`/${name}`, control);
};

module.exports = router;

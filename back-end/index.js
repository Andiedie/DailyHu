const Koa = require('koa');
const bodyParser = require('koa-bodyparser');

const config = require('./config');
const router = require('./app/router');

const app = new Koa();

app.use(bodyParser());
app.use(router.routes());

app.listen(process.env.PORT || 8008);

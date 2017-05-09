const axios = require('axios');
const cheer = require('cheerio');

const service = require('../service');

module.exports = async ctx => {
  ctx.body = service.example();
};

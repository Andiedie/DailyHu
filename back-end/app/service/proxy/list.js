const config = require('./config');
const axios = require('axios');
const assert = require('assert');
const cheerio = require('cheerio');

const getList = async function (site, pageNum) {
  const siteConfig = config[site];

  assert(siteConfig, 'no such site registered.');
  assert(pageNum <= siteConfig.maximumPage, 'page number exceeded.');

  const html = (await axios.get(siteConfig.listUrl(pageNum))).data;
  const $ = cheerio.load(html);
  const res = [];

  $(siteConfig.listItemSelector).each((index, listItem) => {
    res.push(siteConfig.processListItem(listItem));
  });

  return res;
};

module.exports = getList;

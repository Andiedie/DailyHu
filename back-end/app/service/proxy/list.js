const config = require('./config');
const axios = require('axios');
const assert = require('assert');
const cheerio = require('cheerio');

const processHtml = (siteConfig, html) => {
  const $ = cheerio.load(html);
  const res = [];

  $(siteConfig.listItemSelector).each((index, listItem) => {
    res.push(siteConfig.processListItem(listItem));
  });

  return res;
};

const processJson = (siteConfig, data) => {
  const list = siteConfig.processList(data);
  return list.map(siteConfig.processListItem);
};

const getList = async function (site, pageNum) {
  const siteConfig = config[site];

  assert(siteConfig, 'no such site registered.');
  assert(pageNum <= siteConfig.maximumPage, 'page number exceeded.');
  const data = (await axios.get(siteConfig.listUrl(pageNum))).data;

  switch (siteConfig.type) {
    case 'html':
      return processHtml(siteConfig, data);
    case 'json':
      return processJson(siteConfig, data);
  }
};

module.exports = getList;

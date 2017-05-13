const config = require('./config');
const assert = require('assert');
const _ = require('lodash');
const url = require('url');
const extract = require('./extract');
const axios = require('axios');

const map = new Map();
for (const [key, value] of Object.entries(config)) {
  if (value.hostname instanceof Array) {
    value.hostname.forEach(host => {
      map.set(host, key);
    });
  } else {
    map.set(value.hostname, key);
  }
}

const getDetail = async function (src) {
  const hostname = url.parse(src).hostname;
  const name = map.get(hostname);

  assert(name, 'invalid url.');

  const siteConfig = config[name];
  assert(siteConfig, `no such site [${name}] registered.`);

  let data = (await axios.get(src)).data;

  if (siteConfig.detailType === 'json') {
    data = siteConfig.extractHtml(data);
  }

  const extracted = await extract(data, siteConfig.articleSelector);

  if (siteConfig.processArticle) {
    siteConfig.processArticle(extracted);
  }

  return extracted.html();
};

module.exports = getDetail;

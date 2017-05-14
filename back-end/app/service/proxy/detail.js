const config = require('./config');
const assert = require('assert');
const _ = require('lodash');
const url = require('url');
const extract = require('./extract');
const axios = require('axios');

// 域名方向映射到数据源名称，用于getDetail时直接从url分析数据源
const map = new Map();
for (const [key, value] of Object.entries(config)) {
  // 如果是数组，则一个数据源有多个url（比如新浪新闻的子栏目）
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

  // 对于JSON数据，提取其中的html信息
  if (siteConfig.detailType === 'json') {
    data = siteConfig.extractHtml(data);
  }

  // 获得html后，根据selector提取出文章主体
  const extracted = await extract(data, siteConfig.articleSelector);

  // 提取主体后的后续修改
  if (siteConfig.processArticle) {
    siteConfig.processArticle(extracted);
  }

  return extracted.html();
};

module.exports = getDetail;

const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');

module.exports = {
  hostname: 'www.tuicool.com',
  articleSelector: '.article_body',
  listItemSelector: '.list_article_item',
  maximumPage: 20,
  listUrl (pageNum) {
    return `http://www.tuicool.com/ah/0/${pageNum}`;
  },
  processListItem (item) {
    const $item = $(item);
    const res = {};

    res.thumbnail = $item.find('img').attr('src');
    res.title = $item.find('.title a').text();

    const src = $item.find('a').attr('href');
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve(`http://${this.hostname}`, src)}`;

    res.date = $($item.find('.tip span').get(2)).text().replace(/^\s+|\s+$/g, '');

    return res;
  }
};

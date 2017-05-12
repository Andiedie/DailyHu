const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');
const moment = require('moment');

module.exports = {
  type: 'json',
  hostname: 'news-at.zhihu.com',
  articleSelector: '',
  maximumPage: 1000,
  listUrl (pageNum) {
    return `http://news-at.zhihu.com/api/4/news/before/${moment().add(pageNum, 'day').format('YYYYMMDD')}`;
  },
  processList (data) {
    data.stories.forEach(obj => {
      obj.date = data.stories.date;
    });
    return data.stories;
  },
  processListItem (item) {
    const res = {};

    if (item.images) {
      res.thumbnail = (item.images instanceof Array && item.images[0]) || item.images;
    }
    res.title = item.title;
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve('http://news-at.zhihu.com', `/api/4/news/${item.id}`)}`;
    res.date = moment(item.data).format('YYYY-MM-DD');

    return trimAll(res);
  },
  processArticle ($) {
    $('h1').each((index, h1) => {
      const $h1 = $(h1);
      $h1.replaceWith(`<h2>${$h1.text()}</h2>`);
    });

    return $;
  }
};

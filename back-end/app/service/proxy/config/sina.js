const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');
const moment = require('moment');

module.exports = {
  type: 'json',
  hostname: 'news.sina.cn',
  articleSelector: '.art_main_card',
  maximumPage: 1000,
  listUrl (pageNum) {
    return `http://interface.sina.cn/news/feed_top_news.d.json?&page=${pageNum}`;
  },
  processList (data) {
    return data.data;
  },
  processListItem (item) {
    const res = {};

    res.thumbnail = item.img;
    res.type = item.type;   // for test
    res.title = item.title;
    res.url = `${globalConfig.hostname}/detail?url=${item.link}`;
    res.date = item.date;

    return trimAll(res);
  },
  processArticle ($) {
    const keep = $('.art_title_h1, .art_t, .img_wrapper');
    $('body > *').remove();
    $('body').append(keep);

    return $;
  }
};

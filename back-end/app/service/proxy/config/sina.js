const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');
const moment = require('moment');

const config = {
  listType: 'json',
  detailType: 'html',
  hostname: [],
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
    res.title = item.title;

    // 筛掉新浪新闻中的图站和视频站等不好展示的子栏目
    if (config.hostname.indexOf(url.parse(item.link).hostname) === -1) {
      return null;
    }

    res.url = `${globalConfig.hostname}/detail?url=${item.link}`;
    res.date = item.date;

    return trimAll(res);
  },
  processArticle ($) {
    const keep = $('.art_title_h1, .art_t, .img_wrapper, .art_pic_card');
    $('body').empty();
    $('body').append(keep);

    return $;
  }
};

// 新浪新闻的各个子版块的url前缀
['news', 'k', 'mil', 'ent', 'sports', 'finance', 'tech', 'zx']
  .forEach(prefix => {
    config.hostname.push(`${prefix}.sina.cn`);
  });

module.exports = config;

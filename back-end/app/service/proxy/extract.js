const axios = require('axios');
const cheerio = require('cheerio');

/**
 * @param url
 * @return html of the extrated website
 */
const extract = async (url, selector) => {
  const html = (await axios.get(url)).data;
  const $ = cheerio.load(html);

  const main = $(selector);
  $('body > *').remove();
  $('body').append(main);

  $('script, link').remove();

  // 基于腾讯云对象存储加速的基础css
  $('head').append('<link rel="stylesheet" href="http://midterm-1252605895.cosgz.myqcloud.com/modest.css"/>');

  // 绕过防盗链
  $('head').append('<meta name="referrer" content="never">');

  // 屏蔽页面跳转
  // $('a').removeAttr('href').removeAttr('target').removeAttr('rel');

  return $;
};

module.exports = extract;

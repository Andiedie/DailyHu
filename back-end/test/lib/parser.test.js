const axios = require('axios');
const parser = require('../../lib/parser');
const _ = require('lodash');

describe('parser', () => {
  it('work with tuicool', async () => {
    // let html = (await axios.get('http://www.tuicool.com/articles/JFvymqQ')).data;
    // let html = (await axios.get('http://www.tuicool.com/articles/6bqqAjA')).data;
    let html = (await axios.get('http://www.tuicool.com/articles/Abua22A')).data;
    let result = parser.parse(html, {
      // 可以给选择器
      body: '.article_body > div',
      title: '.article_detail_bg > h1:first-child',
      // 也可以给函数处理
      date: $ => {
        const raw = $('.timestamp').text();
        return _.trim(raw.replace('时间', ''));
      }
    });
    console.log(JSON.stringify(result));
  });

  it('work with cnode', async () => {
    let html = (await axios.get('https://cnodejs.org/topic/58d0fb3517f61387400b7e15')).data;
    let result = parser.parse(html, {
      // 可以给选择器
      body: '.markdown-text',
      title: $ => {
        const raw = $('.topic_full_title').text();
        return _.trim(raw.replace('置顶', ''));
      },
      date: '.changes > span:first-child'
    });
    console.log(JSON.stringify(result));
  });
});

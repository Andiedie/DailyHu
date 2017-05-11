const schema = require('./schema/index');
const _ = require('lodash');
const $ = require('cheerio');

const component = {
  p (elem) {
    const res = [];

    elem.children.forEach(child => {
      if (child.type === 'text') {
        if (!child.data.match(/^\s*$/)) {
          res.push(new schema.Node(child.data));
        }
      } else if (child.type === 'tag') {
        const $child = $(child);
        switch (child.name) {
          case 'strong':
            if (!$child.text().match(/^\s*$/)) {
              res.push(new schema.Node($child.text(), child.name));
            }
            break;
          case 'img':
            res.push(new schema.Node($child.attr('src'), 'image'));
            break;
          default:
            if (!$child.text().match(/^\s*$/)) {
              res.push(new schema.Node($child.text()));
            }
        }
      }
    });

    const para = new schema.Paragraph();
    res.forEach(node => {
      node.content = _.trim(node.content);
      para.add(node);
    });

    return para;
  },
};

component.h1 = component.h2 = component.h3 = component.h4 = component.h5 = elem => {
  const pResult = component.p(elem);
  pResult.nodes.forEach(node => {
    node.type = 'strong';
  });

  return pResult;
};

module.exports = component;

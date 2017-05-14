const _ = require('lodash');

/**
 * 工具函数，将对象的所有字段trim一遍
 * @param obj
 * @returns {{}}
 */
module.exports = function (obj) {
  const res = {};
  Object.assign(res, obj);
  for (const [key, value] of Object.entries(res)) {
    res[key] = _.trim(value);
  }
  return res;
};

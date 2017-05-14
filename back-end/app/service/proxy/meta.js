const assert = require('assert');
const config = require('./config');
const axios = require('axios');
const url = require('url');
const globalConfig = require('../../../config');

// 元数据，提供存储在腾讯云COS上的logo和tile图标
const getMetadata = async function () {
  return Object.keys(config)
    .map(name => {
      return {
        name,
        icon: url.resolve(globalConfig.CosStore, `assets/icons/${name}.jpg`),
        tileImage: {
          small: url.resolve(globalConfig.CosStore, `assets/sites/${name}-s.jpg`),
          medium: url.resolve(globalConfig.CosStore, `assets/sites/${name}-m.jpg`),
          large: url.resolve(globalConfig.CosStore, `assets/sites/${name}-l.jpg`),
          wide: url.resolve(globalConfig.CosStore, `assets/sites/${name}-w.jpg`),
        }
      };
    });
};

module.exports = getMetadata;

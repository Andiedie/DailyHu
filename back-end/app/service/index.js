const services = {};

[
  'example'
]
  .forEach(name => {
    services[name] = require(`./${name}`);
  });

module.exports = services;

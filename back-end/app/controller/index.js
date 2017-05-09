const controllers = {};

[
  'example'
]
  .forEach(name => {
    controllers[name] = require(`./${name}`);
  });

module.exports = controllers;

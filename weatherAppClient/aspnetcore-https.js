const https = require('https');
const fs = require('fs');

const options = {
  pfx: fs.readFileSync('path/to/certificate.pfx'),
  passphrase: 'password'
};

module.exports = function (app) {
  app.use((req, res, next) => {
    if (req.secure) {
      next();
    } else {
      res.redirect(`https://${req.headers.host}${req.url}`);
    }
  });

  https.createServer(options, app).listen(443);
};
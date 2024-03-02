'use strict';
require('dotenv').config()
const express = require('express')
const cors = require('cors')
const logger = require('morgan')
const bodyParser = require('body-parser')
const PORT = process.env.PORT
const app = express()
const sharp = require('sharp')
const multer = require('multer')
const path = require('path')
const fs = require("fs")
const util = require('util')
const _ = require('lodash')
const exec = util.promisify(require('child_process').exec)
const photoPath = './public/photos'
const thumbnailPath = './public/thumbnails'

if (!fs.existsSync(photoPath)) {
  fs.mkdirSync(photoPath, { recursive: true })
}

if (!fs.existsSync(thumbnailPath)) {
  fs.mkdirSync(thumbnailPath, { recursive: true })
}

const photo = multer({
  storage: multer.diskStorage({
    destination: function (req, file, cb) {
      cb(null, photoPath)
    },
    filename: function (req, file, cb) {
      cb(null, req.params.file)
    }
  })
})

app.use(cors())
app.use(logger('dev'))
app.use(express.json({ limit: process.env.LIMIT || '50mb' }))
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

app.use('/api/storage/photo', express.static('public/photos'))
app.use('/api/storage/thumbnail', express.static('public/thumbnails'))

app.post('/api/storage/photo/:file', photo.single('file'), (req, res) => {
  res.status(200).json()
})


app.get('/api/storage/photo/:file/resize/:w/:h', async (req, res) => {
  try {
    const { w, h } = req.params
    const { fit, p } = req.query
    const imgPath = path.join(photoPath, req.params.file)

    if (!fs.existsSync(imgPath))
      return res.status(404).json()

    const resizedImage = await sharp(imgPath)
      .resize({ width: parseInt(w), height: parseInt(h), fit, position: p })
      .jpeg()
      .toBuffer()

    res.setHeader('Content-Type', 'image/jpeg');
    res.setHeader('Cache-control', 'no-cache')
    res.end(resizedImage)
  } catch (error) {
    res.status(500).json()
  }
})

app.listen(PORT, () => console.log(`FILE SERVER LISTENING ON ${PORT}`))
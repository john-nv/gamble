import axios from "axios"

const encodeImageFileAsURL = (element) => {
  return new Promise((resolve, reject) => {
    const file = element.files[0]
    const reader = new FileReader()

    reader.onloadend = function () {
      resolve(reader.result)
    }

    reader.readAsDataURL(file)
  })
}


const upload = (key, file) => {
  const formData = new FormData();
  formData.append("file", file);
  return axios.post(`${process.env.REACT_APP_FILE_URL}/api/storage/photo/${key}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

export default {
  encodeImageFileAsURL,
  upload
}
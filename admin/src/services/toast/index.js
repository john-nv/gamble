import _ from 'lodash'
import Swal from 'sweetalert2'
import Toastify from 'toastify-js'

const sucess = (message) =>
  Toastify({
    text: message,
    style: {
      background: "linear-gradient(to right, #49d579, #5ec91e)",
    }
  }).showToast()

const error = (message) =>
  Toastify({
    text: message,
    style: {
      background: "red",
    }
  }).showToast()

const confirm = (options) => {
  let { title, text, icon, confirmButtonText, onConfirmed, showDenyButton } = options
  showDenyButton ??= true
  icon ??= 'warning'
  confirmButtonText ??= 'Đồng ý'

  return Swal.fire({
    title,
    text,
    icon,
    showDenyButton,
    confirmButtonText,
    backdrop: true,
    allowOutsideClick: false,
    denyButtonText: `Đóng`
  }).then(async (result) => {
    if (result.isConfirmed) {
      onConfirmed?.()
    }
  })
}

export default {
  sucess,
  error,
  confirm
}
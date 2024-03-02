import Swal from 'sweetalert2'
import Toastify from 'toastify-js'

const sucess = (message) =>
  Toastify({
    text: message,
    stopOnFocus: true,
    style: {
      background: "linear-gradient(to right, #49d579, #5ec91e)",
    }
  }).showToast()


const error = (message) =>
  Toastify({
    text: message,
    stopOnFocus: true,
    style: {
      background: "red",
    }
  }).showToast()


const confirm = (options) => {
  let { title, text, icon, confirmButtonText, onConfirmed, showDenyButton } = options
  icon ??= 'warning'
  confirmButtonText ??= 'Đồng ý'
  showDenyButton ??= true

  return Swal.fire({
    title,
    text,
    icon,
    allowOutsideClick: false,
    showDenyButton,
    confirmButtonText,
    denyButtonText: `Đóng`
  }).then(async (result) => {
    if (result.isConfirmed) {
      await onConfirmed?.()
    }
  })
}

export default {
  sucess,
  error,
  confirm
}
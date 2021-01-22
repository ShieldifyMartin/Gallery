import Swal from "sweetalert2";

function useAlert(message, isSuccessful) {    
  const isMobile = screen.width <= 700;

  Swal.fire({
    position: isMobile ? "top" : "top-end",
    icon: isSuccessful ? "success" : "error",
    title: message,
    showConfirmButton: false,
    timer: 1500,
    width: isMobile ? 250 : 300,
  });
}

export default useAlert;
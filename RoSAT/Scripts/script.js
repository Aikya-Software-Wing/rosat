$(document).ready(function () {

    // This is a callback function,
    //     fires when a different section slides in

    // Listens for that checkbox and toggles the permanent address section.
    $('#permanentAddrCheck').on('change', function () {

        $('#permanent-address-wrap').slideToggle();

        // TODO: another function to copy the input values from current address to permanent address

    })



});
function sync() {
  var presentAddress=document.getElementById('presentAddress');
  var permanentAddress = document.getElementById('permanentAddress');
  permanentAddress.value = presentAddress.value;

}

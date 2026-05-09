$(document).ready(function() {

    $("#createCustomerForm").submit(function(e) {
        e.preventDefault(); // Prevent the default form submission

        if(ValidateData()){
            SaveCustomer();
        }
    });  


});


function ValidateData(){

    var customerName = $("#CustomerName").val();
    var phoneNumber = $("#Phone").val();
    var email = $("#Email").val();
    var address = $("#Address").val();

    if(customerName === "" || phoneNumber === "" || email === "" || address === ""){
        showToast("يرجى ملء جميع الحقول المطلوبة.","info");
        return false;
    }

    return true;
}


function SaveCustomer() {

    var customerData = {
        customerName: $("#CustomerName").val(),
        phone: $("#Phone").val(),
        email: $("#Email").val(),
        address: $("#Address").val()
    };

    apiAddCustomer(
        customerData,
        function(response) {
            if(response.success){
            showToast("تم إضافة الزبون بنجاح"); 
            
            location.reload();
            } else {
                showToast("فشل في إضافة الزبون: ","error");
            }   
        },
        function(error) {
            showToast("حدث خطأ أثناء إضافة الزبون: ","error");
        }
    );    
    
}
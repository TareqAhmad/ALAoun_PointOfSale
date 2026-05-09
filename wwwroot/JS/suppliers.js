$(document).ready(function() {

    $("#createSupplierForm").submit(function(e) {
        e.preventDefault(); // Prevent the default form submission

        if(ValidateData()){
            SaveSupplier();
        }
    });  


});


function ValidateData(){

    var supplierName = $("#SupplierName").val();
    var phoneNumber = $("#Phone").val();
    var email = $("#Email").val();
    var address = $("#Address").val();

    if(supplierName === "" || phoneNumber === "" || email === "" || address === ""){
        showToast("يرجى ملء جميع الحقول المطلوبة.","info");
        return false;
    }

    return true;
}


function SaveSupplier() {

    var supplierData = {
        supplierName: $("#SupplierName").val(),
        phone: $("#Phone").val(),
        email: $("#Email").val(),
        address: $("#Address").val()
    };

    apiAddSupplier(
        supplierData,
        function(response) {
            if(response.success){
            showToast("تم إضافة المورد بنجاح"); 
            location.reload(); // Reload the page to show the updated list of suppliers
            } else {
                showToast("فشل في إضافة المورد: ","error");
            }   
        },
        function(error) {
            showToast("حدث خطأ أثناء إضافة المورد: " ,"error");
        }
    );    
    
}
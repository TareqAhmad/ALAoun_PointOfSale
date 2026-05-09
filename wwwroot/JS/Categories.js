$(document).ready(function() {

    $("#createCategoryForm").submit(function(e) {
        e.preventDefault(); // Prevent the default form submission

        if(validateCategoryForm()) {
             SaveCategory();
        }

    }); 


});


function validateCategoryForm() {
  
    var name = $("#CategoryName").val();

    if (name === "") {
        showToast("يرجى ملء جميع الحقول المطلوبة بشكل صحيح.","info");
        return false;
    }
    return true;
}

function SaveCategory() {

    var categoryData = {
        categoryName: $("#CategoryName").val()
    };  

    apiAddCategory(
        categoryData,
        function(response) {
            if (response.success) {
               showToast("تم حفظ التصنيف بنجاح");
             
               location.reload(); // Reload the page to reflect the new category
            }
            else 
        {
                showToast("فشل في حفظ التصنيف: ","error");
            }   
        },
        function(error) {
            showToast("حدث خطأ أثناء حفظ التصنيف. يرجى المحاولة مرة أخرى.","error");
        }
    );
}
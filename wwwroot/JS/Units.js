$(document).ready(function() {

    $("#createUnitForm").submit(function(e) {
        e.preventDefault();
    
        SaveUnit(); 
    });
});


function ValidateUnitName(unitName) {
    if (!unitName || unitName.trim() === "") {
        alert("يرجى إدخال اسم الوحدة.");
        return false;
    }
    return true;
}


function SaveUnit(){


    var unitName = $("#UnitName").val();

    var unitData  = {
        unitName: unitName
    };    
    
    if (!ValidateUnitName(unitData.unitName)) {
        showToast("يرجى إدخال اسم الوحدة.","info");
        return;
    }

    apiAddUnit(
        unitData,
        function(data){   
            if(data.success){
                showToast("تمت إضافة الوحدة بنجاح.");
                $("#UnitName").val(""); 
                location.reload();
            } else {
                showToast("فشل في إضافة الوحدة: ","eror");
            }
          },
        function(error){   
            showToast("فشل الاتصال بالخادم، يرجى المحاولة لاحقاً","error"); 
        }
    ); 
}


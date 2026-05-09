$(document).ready(function() {


    GetTaxiesForProduct();
    getCategoriesForProduct();
    GetSuppliersForProduct();
    GetUnitsForProduct();


    $("#createProductForm").submit(function(e) {
        e.preventDefault();    
    
        if (ValidateInputs()) {
            SaveProduct();
        }

    });

    $("#mainUnit").change(function() {
        var mainUnitId = $(this).val();
      
        if (mainUnitId) {
            GetSubUnitsForProduct(mainUnitId);
        } else {
            $("#subUnit").empty();
            $("#subUnit").append(`<option value="">اختر وحدة الصغرى</option>`);
        }   

   });

});


function getCategoriesForProduct(){
 
   apiGetIdAndNameCategories(
        function(categories) {
           
            var categorySelect = $("#categorySelect");
            categorySelect.empty();
            categorySelect.append(`<option value="">اختر تصنيف المنتج</option>`);

            categories.forEach(c=> {
                categorySelect.append(`<option value="${c.id}">${c.name}</option>`);
            });
        },
        function(error) {
            console.error("خطأ في جلب الفئات: ", error);
        }   
    );
}

function GetTaxiesForProduct(){
  
    apiGetIdAndNameTaxies(
        function(taxies) { 
            
            var taxiesSelect = $("#taxiesSelect");
            taxiesSelect.empty();
                taxiesSelect.append(`<option value="">اختر نوع الضريبة</option>`);
            taxies.forEach(t=> {
                taxiesSelect.append(`<option value="${t.id}">${t.name}</option>`);
            });
        },
        function(error) {
            console.error("خطأ في جلب الضرائب: ", error);
        }   
    );
}

function GetSuppliersForProduct(){
    
    apiGetIdAndNameSuppliers(
        function(suppliers) {
           
            var supplierSelect = $("#supplierSelect");
            supplierSelect.empty();
            supplierSelect.append(`<option value="">اختر مورد المنتج</option>`);
            suppliers.forEach(s=> {
                supplierSelect.append(`<option value="${s.id}">${s.name}</option>`);
            });
           },
        function(error) {
            console.error("خطأ في جلب الموردين: ", error);
        }   
    );  
}

function GetUnitsForProduct(){
   
    apiGetIdAndNameUnits(
        function(units) {  
            var unitSelect = $("#mainUnit");
            unitSelect.empty();
            unitSelect.append(`<option value="">اختر وحدة الكبرى</option>`);
            units.forEach(u=> {
                unitSelect.append(`<option value="${u.id}">${u.name}</option>`);
            });
         },
        function(error) {
            alert("خطأ في جلب الوحدات: ", error);
        }   
    );
}

function GetSubUnitsForProduct(mainUnitId){
    
     apiGetIdAndNameUnits(
        function(units) {  
            var unitSelect = $("#subUnit");
            unitSelect.empty();
            unitSelect.append(`<option value="">اختر وحدة الصغرى</option>`);
            units.forEach(u=> {
                if(u.id != mainUnitId){
                    unitSelect.append(`<option value="${u.id}">${u.name}</option>`);
                }
            });
         },
        function(error) {
            alert("خطأ في جلب الوحدات: ", error);
        }   
    )
}   


function ValidateInputs() {
  
    // 1. Fetch and trim values
    var barcode = $("#Barcode").val();
    var name = $("#ProductName").val();
    var cost = parseFloat($("#cost").val());
    var price = parseFloat($("#price").val());
    var quantity = parseInt($("#quantity").val());
    var categoryId = $("#categorySelect").val();
    var taxId = $("#taxiesSelect").val();
    var baseUnitId = $("#mainUnit").val();
    var subUnitId = $("#subUnit").val();
    var conversionFactor = parseFloat($("#conversionFactor").val());
    var supplierId = $("#supplierSelect").val();
    var ReorderLevel = parseInt($("#ProductReorderLevel").val());

    // 2. Check for empty fields
    if (!barcode || !name || isNaN(cost) || isNaN(price) || isNaN(quantity) || !categoryId || !taxId || !supplierId || !baseUnitId || !subUnitId || isNaN(conversionFactor) || isNaN(ReorderLevel)) {
        showToast("يرجى ملء جميع الحقول المطلوبة بشكل صحيح.","info");
        return false;
    }

    // 3. Business Logic Validations
    if (cost < 0 || price < 0 || quantity < 0) {
        showToast("لا يمكن أن تكون القيم (التكلفة، السعر، الكمية) أقل من صفر.","info");
        return false;
    }

    if (price < cost) {
        showToast("سعر البيع أقل من تكلفة المنتج.","info");
        // You can return false here if you want to force a profit margin
    }

    // If all checks pass
    return true;
}

function SaveProduct() {

        var productData = {
            barcode: $("#Barcode").val(),
            productName: $("#ProductName").val(),
            productCost: parseFloat($("#cost").val()),
            productPrice: parseFloat($("#price").val()),
            stockQuantity: parseInt($("#quantity").val()),
            categoryId: $("#categorySelect").val(),
            taxId: $("#taxiesSelect").val(),
            baseUnitId: $("#mainUnit").val(),
            subUnitId: $("#subUnit").val(),
            conversionFactor : parseFloat($("#conversionFactor").val()),
            supplierId: $("#supplierSelect").val(),
            reorderLevel: $("#ProductReorderLevel").val(),
            discountId: 0, // Assuming you have a discount select element
            iconId: 0 // Assuming you have an icon select element

        };

        apiAddProduct(
            productData,
            function(response) { 
                if (response.success) {
                    showToast("تم حفظ المنتج بنجاح");

                    location.reload(); 
                } else {
                    showToast("فشل في حفظ المنتج: ","error");
                }
               },
            function(error) {
                showToast("خطأ في حفظ المنتج: ","error");
            }
        );
}
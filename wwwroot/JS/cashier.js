
/** ---------------
 *  Page Cashier
 *  ---------------
 */

  let allProducts = []; 
  let cart = []; 
  let barcodeBuffer = "";
  let selectedRowProductId = null;
  let globalInvoiceDiscount = 0;


$(document).ready(function(){
   
    if($("#Select-customer-cashier").length === 0 ) return; 
   

    console.log("cashier Page");
    console.log(localStorage.getItem("loginType")); 
    console.log(localStorage.getItem("loginMessage"));
    
    let msg = localStorage.getItem("loginMessage");
   
    if (msg) {
        showToast(msg); 
        localStorage.removeItem("loginMessage");
    }

    initializePage(); 
    setupEventListeners() 

}); 


// --- 2. Initialization ---
function initializePage() {
    $("#barcode").focus();
    loadAllCustomers(); 
    loadAllCategories(); 
    loadAllProducts();
    loadCart(); 
}


// --- 3. Event Listeners (UI Interactions) ---
function setupEventListeners(){

    $("#categoryTabs").on("click", "button", function() {
        $("#categoryTabs button").removeClass("active");
        $(this).addClass("active");
    });

    $("#Select-customer-cashier").on("change", function() {
        localStorage.setItem("customerId", $(this).val());
    });

    // Buttons and Modals
    
    $("#btnPendingInvoice").on("click", AddPendingInvoice);
    $("#btnEditQty").on("click", setEditQtyModal);
    $("#btnDiscount").on("click", setDiscountModal);

    
    $("#holdModal").on("show.bs.modal", CreateBoxesForHold);
    $("#SalesInvoicesModal").on("show.bs.modal",GetAllSalesInvoices)
   
    $("#CancelCart, #btn-Exit").on("click", clearCart);


    $("#btnEndShift").on("click", function () {EndShift(); });
    $("#btnLeave").on("click", function () { EndLeave(); });


    $("#payInvoice").on("click", function() {
        let finalNet = parseFloat($("#netAmount").text()) || 0; 
        let totalBefore = parseFloat($("#totalAmount").text()) || 0; 
        let totalDiscount = parseFloat($("#discountAmount").text()) || 0; 
        let totalTax = parseFloat($("#taxAmount").text()) || 0; 
        openPayModal(finalNet, totalBefore, totalDiscount, totalTax); 
    });

   

    $("#barcode").on("keypress", function(e) {
        if (e.which === 13) {
            let code = $(this).val().trim();
            if (code !== "") handleBarcodeScan(code);
            $(this).val(""); 
            e.preventDefault();
       }
    });

    $(document).on('input', '#receivedAmount', function() {
        let required = parseFloat($("#payFinalAmount").text()) || 0;
        let received = parseFloat($(this).val()) || 0;
        let change = received - required;
        $("#changeAmount").text(change > 0 ? change.toFixed(3) : "0.000");
    });

    $(document).on('click', '#cartTableBody tr', function() {
        $('#cartTableBody tr').removeClass('table-warning');
        $(this).addClass('table-warning');
            selectedRowProductId = $(this).data('id');
    });
}

// --- 4. Cart & Calculations Logic ---

function addToCart(productId) {



    let product = allProducts.find(p => p.productId == productId); 
    if (!product) return;
    
    let existing = cart.find(c => c.productId == productId); 

    if (existing) {
        existing.quantity += 1; 
    }
    else 
    {
        cart.push({
            productId: product.productId,
            productName: product.productName,
            price: (product.productPrice).toFixed(3),
            quantity: 1,
            taxRate:(product.taxRate/100) || 0,
            itemDiscount: 0
        });
    }

    saveCartInLocalStorage(); 
    renderCart(); 
}

function renderCart() {
    
    let currentCart = JSON.parse(localStorage.getItem("cart")) || [];

    let tbody = $("#cartTableBody"); 
    tbody.empty();
    let counter = 0; 

    currentCart.forEach((item) => {
        
        counter++; 
        
        const row = ` 
            <tr data-id="${item.productId}" style="cursor:pointer;"> 
                <td><button class="bi bi-trash-fill text-danger fs-4" onclick="deleteFromCartById(${item.productId})"></button></td>
                <td>${counter}</td>
                <td class="text-end">${item.productName}</td>
                <td class="fw-bold">${item.quantity}</td>
                <td>${item.price}</td>
                <td class="fw-bold">${(item.price * item.quantity).toFixed(3)}</td>
            </tr>`; 
        tbody.append(row);
    });
      calculateTotals(); 
}

function deleteFromCartById(productId){

  let cartTemp = JSON.parse(localStorage.getItem("cart"));
  
  let updateCart = cartTemp.filter(p => p.productId !== productId); 

  localStorage.setItem("cart",JSON.stringify(updateCart)); 

  renderCart();
  
  location.reload(); 

}

function calculateTotals() {
   
    let subTotal = 0, totalTax = 0, itemsDiscountSum = 0;

    cart.forEach(item => {
        let price = parseFloat(item.price) || 0;
        let qty = parseFloat(item.quantity) || 0;
        let taxRate = parseFloat(item.taxRate) || 0;
        let itemDisc = parseFloat(item.itemDiscount) || 0;

        let linePrice = price * qty;
        let afterDisc = linePrice - itemDisc;
        let lineTax = Math.max(0, afterDisc) * taxRate;

        subTotal += linePrice;
        itemsDiscountSum += itemDisc;
        totalTax += lineTax;
    });

    let totalDiscountAmount = itemsDiscountSum + (parseFloat(globalInvoiceDiscount) || 0);
    let netAmount = (subTotal - totalDiscountAmount) + totalTax;

    $("#totalAmount").text(subTotal.toFixed(3));
    $("#discountAmount").text(totalDiscountAmount.toFixed(3));
    $("#taxAmount").text(totalTax.toFixed(3));
    $("#netAmount").text(netAmount.toFixed(3));
}

// --- 5. Storage & State Management ---

function saveCartInLocalStorage() {
    localStorage.setItem("cart", JSON.stringify(cart)); 
    let customerId = $("#Select-customer-cashier").val() || 0; 
    localStorage.setItem("customerId", customerId); 
}

function loadCart() {
    let storedCart = localStorage.getItem("cart"); 
    let customerId = parseInt(localStorage.getItem("customerId")) || 1; 

    if (storedCart) {
        cart = JSON.parse(storedCart); 
        renderCart(); 
    }
    loadAllCustomers(customerId);
}

function clearCart() {
    cart = []; 
    renderCart(); 
    localStorage.removeItem("cart");
    localStorage.removeItem("customerId");
    ["#totalAmount", "#discountAmount", "#taxAmount", "#netAmount"].forEach(id => $(id).text("0.000"));
}
    
// --- 6. API Services (Ajax Calls) --- 

function loadAllCustomers(selectCustomerId = 1) {
    
        let select = $("#Select-customer-cashier");

        apiGetIdAndNameCustomers(
        function(data) {
            select.empty();
         
            data.forEach(customer=>{
                select.append(`<option value="${customer.id}">${customer.name}</option>`);
                });
                select.val(selectCustomerId);
        }
        ,function(error) {
                showToast("فشل في تحميل قائمة الزبائن من الخادم","error");
        }
    )
}

function loadAllCategories() {

    let container = $("#categoryTabs");

    apiGetIdAndNameCategories(
        function(data) {
            container.empty();
            
            container.append(`
                                <button class="btn btn-light border rounded active" 
                                        style="width:10rem; height:7rem;" 
                                        onclick="displayProducts()"> الكل
                                </button>`
                                );

            data.forEach(category=>{
                container.append(`
                                    <button class="btn btn-light border rounded" 
                                            style="width:8rem; height:7rem;" 
                                            onclick="filterCategory(${category.id})"> ${category.name}
                                    </button>`
                                );


            });
        },
        function(error){
            showToast("فشل في تحميل قائمة الاصناف من الخادم","error")
        }
    )
    
}

function loadAllProducts() {
    
    apiGetProductsForOperations(
        function(data) {
           allProducts = data; 
             displayProducts();
        },
        function(error)
        {
            showToast("فشل في تحميل قائمة المواد من الخادم","error")
        }
    )

}

function GetAllSalesInvoices() {
    
    apiGetAllSalesInvoices(
    function(data){
            renderSalesGrid(data);
    },
    function(error) {
        showToast("فشل في تحميل قائمة فواتير المبيعات من الخادم","error")
    }
    )

}

function SaveInvoice() {
    if (cart.length === 0) {
        showToast("لا يمكن حفظ فاتورة فارغة","info");
        return;
    }

    let invoiceData = {
        customerId: parseInt(localStorage.getItem("customerId")),
        items: cart, // المصفوفة تحتوي الآن على الـ taxRate لكل صنف
        totalBefore: parseFloat($("#totalAmount").text()),
        totalTax: parseFloat($("#taxAmount").text()),
        netAmount: parseFloat($("#netAmount").text())
    };

    $.ajax({
        url: "/SalesInvoices/Create",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(invoiceData),
        success: function(response) {
            showToast("تم حفظ الفاتورة وإصدار الرقم: " + response.invoiceId);
            clearCart(); // تصفير السلة بعد النجاح
            if (response.invoiceId) printInvoice(response.invoiceId);
        },
        error: function() {
            showToast("فشل في الاتصال بالسيرفر لحفظ الفاتورة","error");
        }
    });
}

function AddPendingInvoice() {
    
    if (cart.length === 0) {
        showToast("السلة فارغة، لا يمكن تعليق طلب فارغ","info");
        return;
    }

    var dataPending = {
        customerId: parseInt(localStorage.getItem("customerId")) || 1,
        items: cart, // السلة تحتوي الآن على بيانات المنتج والضريبة والكمية
        sumAmount: parseFloat($("#totalAmount").text()) || 0,
        sumDiscount: parseFloat($("#discountAmount").text()) || 0,
        sumTax: parseFloat($("#taxAmount").text()) || 0,
        netInvoice: parseFloat($("#netAmount").text()) || 0
    };

    apiAddPendingInvoice(
        dataPending,
        function(response){
                if (response === true || response.success === true) {
                cart = []; 
                localStorage.removeItem("cart"); 
                localStorage.removeItem("customerId"); 
                showToast("تم حفظ الطلب في المعلقات بنجاح","error");
                location.reload();
            } else {
                showToast("فشل السيرفر في معالجة طلب التعليق","error");
            }
        },
        function(error){
                showToast( " خطأ في الاتصال بالسيرفر أثناء تعليق السلة","error");
        }

    )

}

``
function saveQuickProduct(productData) {
    return $.ajax({
        url: "/Products/Create",
        method: "POST",
        data: productData
    });
}


// --- 7. Helper & UI Functions ---

function renderProductCards(productsArray) {
    let container = $("#cashier-products");
    container.empty();
    
    if (productsArray.length === 0) {
        container.append('<div class="w-100 text-center" style="min-height: 20rem;"><h3>لا يوجد منتجات</h3></div>');
        return;
    }

    productsArray.forEach(p => {
        container.append(`
            <div class="btn btn-light border rounded d-flex flex-column p-1" style="width:10rem; height:8rem;" onclick="addToCart(${p.productId})">
                <div class="flex-grow-1 d-flex justify-content-center align-items-center">
                    <img src="/Images/${p.iconId}.png" onerror="this.src='/Images/default.png'" style="max-height:60px; max-width:100%;">
                </div>
                <div class="text-center fw-bold text-wrap bg-alter">${p.productName}</div>
            </div>`);
    });
}

function displayProducts() { renderProductCards(allProducts); }


function filterCategory(categoryId) {
    let filtered = allProducts.filter(p => p.categoryId === categoryId);
    renderProductCards(filtered);
}

function openPayModal(net, before, discount, tax) {
    $("#payFinalAmount").text(net.toFixed(3));
    $("#payTotalBefore").text(before.toFixed(3));
    $("#payTotalDiscount").text(discount.toFixed(3));
    $("#payTotalTax").text(tax.toFixed(3));
    $("#receivedAmount").val(net.toFixed(3));
    $("#changeAmount").text("0.000");
    $('#payModal').modal('show');
}

function renderSalesGrid(data) {
    
    const tbody = $("#sales-grid-content");
    tbody.empty(); 

    let totalSum = 0;
        
    console.log("renderSalesGrid");
    
    if (!data || data.length === 0) {
        tbody.append('<tr><td colspan="7" class="text-center py-4 text-muted">لا توجد مبيعات مسجلة حالياً</td></tr>');
        $("#salesTotalCount").text(0);
        $("#salesTotalSum").text("0.000");
        return;
    }

    
    data.forEach(inv => {
        totalSum += inv.netAmount;
        
        tbody.append(`
            <tr>
                <td class="fw-bold text-primary">#${inv.invoiceId}</td>
                <td><small>${inv.invoiceDate}</small></td>
                <td>${inv.customerName || 'زبون عام'}</td>
                <td>
                    <span class="badge bg-info-subtle text-info border border-info-subtle">
                        ${inv.paymentType || 'نقدي'}
                    </span>
                </td>
                <td class="fw-bold">${parseFloat(inv.netAmount).toFixed(3)}</td>
                <td><small class="text-muted">${inv.userName || 'System'}</small></td>
                <td class="text-center">
                    <div class="btn-group">
                        <button class="btn btn-sm btn-outline-primary" title="طباعة" onclick="printInvoice(${inv.invoiceId})">
                            <i class="bi bi-printer"></i>
                        </button>
                        <button class="btn btn-sm btn-outline-secondary" title="عرض التفاصيل" onclick="viewInvoiceDetails(${inv.invoiceId})">
                            <i class="bi bi-eye"></i>
                        </div>
                    </div>
                </td>
            </tr>
        `);
    });

    // تحديث ملخص الفواتير في أسفل المودال أو الصفحة
    $("#salesTotalCount").text(data.length);
    $("#salesTotalSum").text(totalSum.toFixed(3));
}

function printInvoice(id) {
    console.log("جاري تحضير الطباعة للفاتورة: " + id);
    // فتح صفحة الطباعة في تبويب جديد
    window.open('/SalesInvoices/Print/' + id, '_blank');
}

function viewInvoiceDetails(id) {
    // يمكنك هنا استدعاء Ajax لجلب الأصناف الخاصة بهذه الفاتورة وفتح مودال آخر
    console.log("عرض تفاصيل الفاتورة رقم: " + id);
}

function CreateBoxesForHold() {
        
    var container = $("#hold-content"); 
    container.html('<div class="text-center w-100"><div class="spinner-border text-primary"></div><p>جاري التحميل...</p></div>');
        
    apiGetAllPendingInvoices(
        function(data) {
            container.empty(); 
            
            if (!data || data.length === 0) {
                container.append('<div class="alert alert-info w-100 text-center">لا توجد طلبات معلقة حالياً</div>');
                return;
            }

            data.forEach(pi => {
                let isOccupied = pi.status == 1;
                let statusBadge = isOccupied 
                    ? '<span class="badge bg-success">مشغولة</span>' 
                    : '<span class="badge bg-secondary">فارغة</span>';
                
                let iconClass = isOccupied ? 'bi-cart-fill text-success' : 'bi-cart text-muted';

                container.append(`
                    <div class="hold-item btn btn-light border rounded d-flex flex-column p-2 m-1 shadow-sm" 
                        style="width:10rem; height:9rem; cursor:pointer; transition: 0.3s;" 
                        onclick="loadPendingInvoice(${pi.pendingInvoiceId})"> 

                        <div class="flex-grow-1 d-flex flex-column justify-content-center align-items-center">
                            <i class="bi ${iconClass} fs-1"></i>
                            <span class="fw-bold mt-2">طاولة ${pi.pendingInvoiceId}</span>
                            <small class="text-primary fw-bold">${parseFloat(pi.netInvoice || 0).toFixed(3)}</small>
                        </div>

                         <div class="mt-2 small text-center">
                        ${statusBadge}
                    </div>
                </div> 
                     `);    
             });
        },
        function(error) {
        container.html('<div class="alert alert-danger w-100">فشل في تحميل البيانات، تأكد من الاتصال بالسيرفر</div>');

       }
    )
    

}

function EndShift() {
    if (confirm("هل أنت متأكد من إنهاء الوردية (الشفت)؟ سيتم طباعة تقرير الإغلاق.")) {

           transLogin = {
                userId:  window.PosSession.userId,
                userName: window.PosSession.userName,
                logTypeId : 2,
                notes:"تسجيل حركة انهاء الدوام",
           }
   
         apiSetAuditLogin(
            transLogin,
            function(response){
                if(response.success)
                {
                  localStorage.setItem("loginType",response.loginType); 
                  localStorage.setItem("loginMessage",response.message); 
                  window.location.href = "/Home/Index";
                }
            },
            function(error){
                showToast("حدث خطا اثناء تسجيل حركة المغادرة","error");
            }
        )
       
    }
}

function EndLeave() {
   
    transLogin = {
            userId:  window.PosSession.userId,
            userName: window.PosSession.userName,
            logTypeId : 3,
            notes:"تسجيل حركة مفادرة",
    }
   
    apiSetAuditLogin(
            transLogin,
            function(response){
                if(response.success)
                {
                  localStorage.setItem("loginType",response.loginType); 
                  localStorage.setItem("loginMessage",response.message); 
                  window.location.href = "/Home/Index";
                }
            },
            function(error){
                showToast("حدث خطا اثناء تسجيل حركة المغادرة","error");
            }
        )

      
}

/**
 *  ---------------------------------
 *   Modal Logic & Other UI Interactions
 *  ----------------------------------
 */

function setHoldModal() {


}


function setEditQtyModal() {
    
     if(cart.length === 0 )
    { 
        showToast("السلة فارغة","error");
        return false; 
    }
    if (selectedRowProductId === null) {
        showToast("يرجى اختيار مادة من الجدول أولاً","info");
        return false; 
    }


    let item = cart.find(x => x.productId == selectedRowProductId);
    if (item) {
        $("#editQtyProductName").text(item.productName);
        $("#newQtyInput").val(item.quantity);
    }

    $("#editQtyModal").modal('show');

}

function setDiscountModal() {
       
    if (cart.length === 0) {
            showToast("السلة فارغة!","error");
            return false;
    }

    // تفعيل/تعطيل خيار المادة بناءً على الاختيار في الجدول الرئيسي
    if (selectedRowProductId) {
        $("#scopeItem").prop("disabled", false);
        $("#lblScopeItem").removeClass("text-muted");
        $("#scopeItem").prop("checked", true); // اجعله الاختيار الافتراضي إذا كان هناك مادة مختارة
    } else {
        $("#scopeItem").prop("disabled", true);
        $("#lblScopeItem").addClass("text-muted");
        $("#scopeInvoice").prop("checked", true);
    }

    refreshDiscountModalTable();
    
    $("#discountModal").modal('show');



}





 //-------------------------------------------------------

// مراقب المدخلات لحساب المتبقي
$(document).on('input', '#receivedAmount', function() {
    let required = parseFloat($("#payFinalAmount").text()) || 0;
    let received = parseFloat($(this).val()) || 0;
    let change = received - required;
    
    $("#changeAmount").text(change > 0 ? change.toFixed(3) : "0.000");
});


// btn EditQty 

$(document).on('click', '#cartTableBody tr', function() {

    $('#cartTableBody tr').removeClass('table-warning'); // إزالة التحديد السابق
    $(this).addClass('table-warning'); // تلوين الصف المختار 
    selectedRowProductId = $(this).data('id'); // تأكد أن صف الجدول لديه data-id
});





function numpadInput(val) {
    let input = $("#newQtyInput");
    if (val === 'C') {
        input.val("");
    } else {
        input.val(input.val() + val);
    }
}

function changeQtyValue(val) {
    let input = $("#newQtyInput");
    let current = parseInt(input.val()) || 0;
    if (current + val >= 1) input.val(current + val);
}

function updateCartQuantity() {
    
    let newQty = parseInt($("#newQtyInput").val());
    if (isNaN(newQty) || newQty < 1) {
        showToast("الكمية يجب أن تكون 1 على الأقل","info");
        return;
    }

    let index = cart.findIndex(x => x.productId == selectedRowProductId);
    if (index !== -1) {
        console.log(cart[index]);
        cart[index].quantity = newQty;
        renderCart(); 
        $("#editQtyModal").modal('hide');
    }
}

// Btn Discount 

function refreshDiscountModalTable() {
      
      

        let tbody = $("#discountProductsTable");
        let amountDisplay = $("#amountBeforeDiscount");
        tbody.empty();
        
        let isGlobal = $("#scopeInvoice").is(":checked");
        let targetProducts = [];
        let totalTargetAmount = 0;

        if (isGlobal) {
            // إذا كان الخصم على الفاتورة، نعرض كل السلة
            targetProducts = cart;
        } else if (selectedRowProductId) {
            // إذا كان على مادة معينة، نبحث عن تلك المادة فقط
            let item = cart.find(x => x.productId == selectedRowProductId);
            if (item) targetProducts = [item];
        }
        console.log(targetProducts);

        targetProducts.forEach(item => {
            let lineTotal = item.price * item.quantity;
            totalTargetAmount += lineTotal;
            
            tbody.append(`
                <tr>
                    <td class="text-end px-3">${item.productName}</td>
                    <td class="text-center">${item.quantity}</td>
                    <td class="text-center text-primary fw-bold">${lineTotal.toFixed(3)}</td>
                </tr>
            `);
        });

     amountDisplay.text(totalTargetAmount.toFixed(3));
}



//Barcode Scanner

$("#barcode").on("keypress", function(e) {
    if (e.which === 13) { // مفتاح Enter
        let code = $(this).val().trim();
        if (code !== "") {
            handleBarcodeScan(code);
        }
        $(this).val(""); // تصفير الحقل للمسحة القادمة
        e.preventDefault(); // منع أي سلوك افتراضي للـ Enter
    }
});

function handleBarcodeScan(code) {
        // البحث في مصفوفة المنتجات المحملة مسبقاً
        // ملاحظة: تأكد أن حقل الباركود في قاعدة البيانات يسمى barcode
        let product = allProducts.find(p => p.barcode === code);

        if (product) {
            addToCart(product.productId);
            // تمرير تركيز الماوس للحقل مرة أخرى لضمان الاستمرارية
            $("#barcode").focus();
        } else {
            //  في حال عدم وجود المنتج
            openQuickAddModal(code);
        }
}

function openQuickAddModal(code) {
        
            $("#newProductBarcode").val(code); // وضع الباركود الممسوح في الخانة
            $("#newProductName").val("");
            $("#newProductPrice").val("");
            
            // تعبئة قائمة التصنيفات من البيانات الموجودة مسبقاً في نظامك
            let catSelect = $("#newProductCategory");
            catSelect.empty();
            // نعتمد هنا على دالة لجلب التصنيفات أو استغرامها من الـ UI
            $("#categoryTabs button").each(function() {
                let catId = $(this).attr("onclick")?.match(/\d+/);
                if(catId) {
                    catSelect.append(`<option value="${catId[0]}">${$(this).text().trim()}</option>`);
                }
            });

            $("#quickAddProductModal").modal("show");
}

function saveQuickProduct() {
            let productData = {
                productName: $("#newProductName").val(),
                productPrice: parseFloat($("#newProductPrice").val()),
                barcode: $("#newProductBarcode").val(),
                categoryId: parseInt($("#newProductCategory").val()),
                iconId: 1 // أيقونة افتراضية
            };

            if (!productData.productName || !productData.productPrice) {
                showToast("يرجى إكمال البيانات","info");
                return;
            }

            $.ajax({
                url: "/Products/Create", // تأكد من وجود Action في الكنترولر بهذا الاسم
                method: "POST",
                data: productData,
                success: function(newProduct) {
                    // 1. إضافة المنتج الجديد للمصفوفة العامة لكي لا يسأل عنه مرة أخرى
                    allProducts.push(newProduct);
                    
                    // 2. إضافته للسلة فوراً
                    addToCart(newProduct.productId);
                    
                    // 3. إغلاق النافذة وتصفير الحقول
                    $("#quickAddProductModal").modal("hide");
                    $("#barcode").val("").focus();
                },
                error: function() {
                    showToast("فشل حفظ المنتج الجديد","error");
                }
            });
}





function playSuccessSound() {
        let audio = new Audio('/sounds/beep.mp3');
        audio.play();
}


// ---------------------------------------------------



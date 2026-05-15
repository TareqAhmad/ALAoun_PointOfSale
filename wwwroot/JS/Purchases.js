

let allProducts = []; 
let CartPurchases = []; 
let subTotalInvoice = 0; 
let totalDiscountInvoice = 0; 
let totalTaxInvoice = 0; 
let netInvoice = 0; 



$(document).ready(function(){


    initializePage()


    $("#productSearch").on("keypress",function(e){
       
        if(e.which === 13)
        {
            let code = $(this).val().trim(); 
            if(code !== "") handleBarcodeScan(code);
            $(this).val(""); 
            e.preventDefault();
        }


    }); 

    $("#btnSave").on("click",SavePurchaseInvoice); 


}); 



// --- 1. Initialization ---
function initializePage() {
    $("#productSearch").focus();
    loadAllSuppliers(); 
    loadAllProducts();
    loadAllPaymentMethods(); 
    renderCart(); 
    
}


function loadAllSuppliers(){

    apiGetIdAndNameSuppliers(
        function(data){
             var select = $("#supplierSelect");
                select.empty(); 
                select.append(`<option value="">اختر المورد...</option>`); 

             data.forEach(s => {
                select.append(`<option value="${s.id}">"${s.name}"</option>`);
             }); 

        },
        function(error)
        {
            showToast("خطا في تحميل الموردين");
        }

    )
    

}

function loadAllProducts(){

    apiGetProductsForOperations(
        function(data){
            console.log(data); 
              allProducts = data; 
              console.log(allProducts);
        },
        function(error){

        }
    )
}

function loadAllPaymentMethods(){
    
    apiGetIdAndNamePaymentMethods(
        function(data){
           let select = $("#paymentMethod"); 
           select.empty(); 
           select.append(`<option value="">اختر طريقة الدفع...</option>`); 

             data.forEach(s => {
                select.append(`<option value="${s.id}">"${s.name}"</option>`);
             }); 

        },
        function(error){
            showToast("خطا في تحميل طرق الدفع")
        }
    )
}

function handleBarcodeScan(code){


    console.log(code);
    let product = allProducts.find(p=> p.barcode === code);

    if(product){
        addToCartPurchase(product.productId); 
        $("#productSearch").focus();
    }
    else{
        showToast("المنتج غير معرف في النظام")
    }
  
}

//----------------------------------------------

function addToCartPurchase(productId)
{
    let product = allProducts.find(p => p.productId == productId); 
    if(!product) return; 

   let existingProduct = CartPurchases.find(c => c.productId == product.productId); 

   if(existingProduct){
        existingProduct.quantity +=1; 
   }
   else{
        CartPurchases.push({
                    productId: product.productId,
                    productName: product.productName,
                    purchasePrice: product.purchasePrice,
                    quantity: 1,
                    tax: product.taxRate || 0,
                    discount: product.discountRate || 0
        }); 
    }
  
   saveCartInLocalStorage(); 
   renderCart();

}

function saveCartInLocalStorage() {
    localStorage.setItem("CartPurchases", JSON.stringify(CartPurchases));  
    let supplierId =parseInt($("#supplierSelect").val() || 0); 
    localStorage.setItem("supplierId", supplierId); 
    let paymentId =parseInt($("#paymentMethod").val() || 0); 
    localStorage.setItem("paymentId",paymentId); 

}

function renderCart() {

    let currentCart = JSON.parse(localStorage.getItem("CartPurchases") || []); 
   
    subTotalInvoice = 0;
    totalDiscountInvoice = 0;
    totalTaxInvoice = 0;
    netInvoice = 0;
    let counter = 0; 
    
    let tbody = $("#purchaseItemsBody"); 
    tbody.empty();

    currentCart.forEach((item) => {

        let rowTotal = calculateRowTotal(item.quantity, item.purchasePrice, item.tax, item.discount);

        const row = ` 
            <tr data-id="${item.productId}"> 
                <td class="text-center"><button class="bi bi-trash-fill text-danger fs-3" onclick="deleteFromCartById(${item.productId})"></button></td>
                <td class= "text-center">${++counter}</td>
                <td class="text-end">${item.productName}</td>
                <td class="fw-bold">${item.quantity}</td>
                <td>${item.purchasePrice}</td>
                <td>${item.discount}</td>
                <td>%${item.tax}</td>
                <td class="fw-bold">${rowTotal.toFixed(3)}</td>
            </tr>`; 
        tbody.append(row);
    });

    // 3. تحديث واجهة الإجماليات النهائية بعد انتهاء الحلقة
    updateInvoiceSummaryLabels();
}

function deleteFromCartById(productId){

  let cartTemp = JSON.parse(localStorage.getItem("cart"));
  
  let updateCart = cartTemp.filter(p => p.productId !== productId); 

  localStorage.setItem("cart",JSON.stringify(updateCart)); 

  renderCart();
  
  location.reload(); 

}

function calculateRowTotal(quantity, price, taxRate, discountAmount) {
    let qty = parseFloat(quantity || 0);
    let unitPrice = parseFloat(price || 0);
    let tax = parseFloat(taxRate || 0);
    let disc = parseFloat(discountAmount || 0);

    let subTotal = qty * unitPrice;
    let afterDiscount = subTotal - disc;
    let taxValue = afterDiscount * (tax / 100);
    let totalLine = afterDiscount + taxValue;

    // إضافة قيم هذا السطر للإجماليات العامة للفاتورة
    subTotalInvoice += subTotal;
    totalDiscountInvoice += disc; // هنا نجمع الخصم نفسه وليس السعر بعد الخصم
    totalTaxInvoice += taxValue;
    netInvoice += totalLine;

    return totalLine;
}


function clearCart() {
    CartPurchases = []; 
    renderCart(); 
    localStorage.removeItem("CartPurchases");
    localStorage.removeItem("supplierId");
    ["#totalAmount", "#discountAmount", "#taxAmount", "#netAmount"].forEach(id => $(id).text("0.000"));
}

// --------------------------------------------------------


function updateInvoiceSummaryLabels() {
    $("#txtPurchaseTotal").text(subTotalInvoice.toFixed(3)); 
    $("#txtDiscountAmount").text(totalDiscountInvoice.toFixed(3)); 
    $("#txtTaxAmount").text(totalTaxInvoice.toFixed(3)); 
    $("#txtNetAmount").text(netInvoice.toFixed(3)); 
}


function SavePurchaseInvoice(){
   
   var purchaseData = {
          supplierId :$("#supplierSelect").val(),
          paymentId  : $("#paymentMethod").val(),
   }; 

   apiAddPurchaseInvoice(
      purchaseData,
      function(response){
           if(response.success){
                 localStorage.removeItem(); 
                 showToast(response.message);
           }else{
                showToast(response.message); 
           }
      },
      function(error){
               showToast("فشل الحفظ من الخادم");
      }



   ); 


}
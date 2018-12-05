// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function(){

    var foot = $('footer')[0];
    if(location.href.indexOf('Administrador') > -1 || location.href.indexOf('Usuario') > -1){
        foot.style.display = "none";
    }

    /* LOGIN */ 
    
    var btnlogout = $('#btn-logout')[0];
    var formlogout = $('#logoutForm')[0];
        
    if(btnlogout){
        btnlogout.addEventListener('click',(e)=>{
            preventDefaults(e);
    
            formlogout.submit();
    
        });
    }

    /* REGISTRO */
    var nombre = $('#name').val();
    var apellido = $('#lastname').val();
    var correo = $('#email').val();
    var pass = $('#password').val();
    var terminos = $('#terms').val();
    if (nombre != "" && apellido != "" && correo != "" && pass != "" && terminos != false) {
        const urlregistro = location.protocol + "//" + location.hostname + "/Registro";
        let datos = {
            'name':nombre,
            'lastname':apellido,
            'email':correo,
            'password':pass,
            'terms':terminos
        };
        $.ajax({
            url:urlregistro,
            data:datos,
            success:function(response){
                if (response) {
                    //hacer algo
                }
            },
            error:function(error){
                if (error) {
                    //ver cual es el error
                }
            }
        });
    }

    /* PUBLICAR */

    var titulo = $('#Titulo').val();
    var precio = $('#PrecioVenta').val();
    var descripcion = $('#Descripcion').val();
    var estado = $('#Estado').val();
    var divdrop = $('.drop-files')[0];
    var div = $('.publicar-fotos')[0];
    var btnupload = $('#btn-upload')[0];
    var btnfiles = $('#files-upload')[0];
    var data = 0;
    var imgprod = [];
   
        if (div) {
            ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
                div.addEventListener(eventName, preventDefaults, false);
            });
            div.addEventListener('drop', drop, false);
            div.addEventListener('dragover',(e)=>{
                div.style.color = "green";
            });
            div.addEventListener('dragleave',(e)=>{
                div.style.color = "orange";
            });
            btnupload.addEventListener('click',(e)=>{
                preventDefaults(e);
                btnfiles.click();
            });
            btnfiles.addEventListener('change',(e)=>{
                let file = btnfiles.files;
                imgprod = file;
                mostrarArchivo(file);
            });
        }
    

    function drop(e) {
        let dt = e.dataTransfer;
        btnfiles.files=dt.files;
        div.style.color = "rgba(0,0,0,.3)";
        mostrarArchivo(dt.files); 
    }
 
    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    function mostrarArchivo(file){

        function volver(){
            divdrop.childNodes[1].innerText = "Arrastra tus fotos";
        }

        var reader = new FileReader();
        var reader2 = new FileReader();
        var reader3 = new FileReader();
            
        if (file[0].type.startsWith('image/')) {
            if (file.length == 1) {
                let divimg = document.createElement('div');
                divimg.setAttribute('class','img-upload');
                divdrop.innerHTML='';
                div.style.padding = '2% 0 2% 0';
                if (file[0]) {
                    reader.readAsDataURL(file[0]);
                }
                reader.onloadend = function (){
                    divimg.style.backgroundImage= "url("+reader.result+")";
                    divdrop.appendChild(divimg); 
                    imgprod.unshift(file[0]);
                }
            }else if(file.length == 2){
                let divimg1 = document.createElement('div');
                let divimg2 = document.createElement('div');
                divimg1.setAttribute('class','img-upload-2');
                divimg2.setAttribute('class','img-upload-2');
                divdrop.innerHTML='';
                div.style.padding = '2% 0 2% 0';
                if (file[0]) {
                    reader.readAsDataURL(file[0]);
                    reader.onloadend = function (){
                        divimg1.style.backgroundImage= "url("+reader.result+")";
                        divdrop.appendChild(divimg1);
                        imgprod.unshift(file[0]);
                    }
                }
                if (file[1]) {
                    reader2.readAsDataURL(file[1]);
                    reader2.onloadend = function (){
                        divimg2.style.backgroundImage= "url("+reader2.result+")";
                        divdrop.appendChild(divimg2); 
                        imgprod.unshift(file[1]);
                    }
                }
            }else if (file.length == 3) {
                let divimg1 = document.createElement('div');
                let divimg2 = document.createElement('div');
                let divimg3 = document.createElement('div');
                divimg1.setAttribute('class','img-upload-3');
                divimg2.setAttribute('class','img-upload-3');
                divimg3.setAttribute('class','img-upload-3');
                divdrop.innerHTML='';
                div.style.padding = '2% 0 2% 0';
                if (file[0]) {
                    reader.readAsDataURL(file[0]);
                    reader.onloadend = function (){
                        divimg1.style.backgroundImage= "url("+reader.result+")";
                        divdrop.appendChild(divimg1);
                        imgprod.unshift(file[0]);
                    }
                }
                if (file[1]) {
                    reader2.readAsDataURL(file[1]);
                    reader2.onloadend = function (){
                        divimg2.style.backgroundImage= "url("+reader2.result+")";
                        divdrop.appendChild(divimg2);
                        imgprod.unshift(file[1]); 
                    }
                }
                if (file[2]) {
                    reader3.readAsDataURL(file[2]);
                    reader3.onloadend = function (){
                        divimg3.style.backgroundImage= "url("+reader3.result+")";
                        divdrop.appendChild(divimg3);
                        imgprod.unshift(file[2]);
                    }
                }
            }else{
                divdrop.childNodes[1].innerText = "Solo se pueden subir 3 imagenes.";
            setTimeout(volver,5000);
            }
        } else {
            divdrop.childNodes[1].innerText = "Solo se aceptan imagenes.";
            setTimeout(volver,5000);
        }

    }

    var btnsend = $('#btn-send')[0];
    var formpub = $('#form-pub')[0];
    if (btnsend) {
        btnsend.addEventListener('click',saveProduct,false);

        function saveProduct(e){
            preventDefaults(e);
            
            formpub.submit();
                  
        }
    }

    var btnedit = $('#btn-edit')[0];
    var btnedit2 = $('#btn-edit-2')[0];
    var inputs = document.querySelectorAll('input');
    if(btnedit){
        btnedit.addEventListener('click',(e)=>{
            
            preventDefaults(e);
              
            for (let i = 1; i < inputs.length; i++) {
                inputs[i].readOnly = false;
                inputs[i].disabled = false;
            }

            btnedit.style.display = "none";
            btnedit2.style.display = "block";
        });
    }

    /*  */

});
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let inputPhone = document.getElementById('phone');

inputPhone.oninput = () => {
    val = inputPhone.value.split('');
    keys = [9, 12]
    
    if(val.length === 1){
        val.push('(');
    }
    
    if(val.length === 5){
        val.push(')');
    }
    
    if(keys.includes(val.length)){
        val.push('-')
    }
    inputPhone.value = val.join('');
};
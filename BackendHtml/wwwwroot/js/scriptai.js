
    function generateRecipe() {
      alert("This is a placeholder function. Recipe generation will happen here.");
    }
  

    let topBtn = document.getElementById("topBtn");
    window.onscroll = function() {
      topBtn.style.display = document.documentElement.scrollTop > 100 ? "block" : "none";
    };
    function topFunction() {
      document.documentElement.scrollTop = 0;
    }
  
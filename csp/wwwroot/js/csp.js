var currentCount = 0;
var $counter = document.getElementById("$counter");
var $tmpl = _.template("Count: <%= amount %>");

function updateCount() {
    currentCount += 1
    $counter.innerHTML = $tmpl({ amount: currentCount });
}
tab = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]
var i, chunk = 5, chunks = [];
for (i = 0; i < tab.length; i += chunk) {
    chunks.push(tab.slice(i, i + chunk));    
}

//usage for Oracle query

var query = "("
for (var i = 0; i < chunks.length; i++) {
	if (i > 0) query += " OR "
	query += "E.ID IN (" + chunks[i].toString() + ")"; 
}
query += ")"

'use strict'

module.exports.stringToConcat = (expr) => {
    let res = []
    if (expr.length > 1) {

        res.push(expr.substring(0, 1));
        for (let i = 1; i < expr.length; i++) {
            res.push(expr.substring(i, 1))
            res.push('Concat')
        }
        return res
    }

    return null
}

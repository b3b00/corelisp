(defun caar 
   (x) (car (car x))
) 

(defun cadar 
   (x) (car (cdr (car x)))
) 

(defun null (x)
	(eq x '())
)

(defun assoc (x y)
    (cond 
    	((null y) 'nil)
        ((eq x (caar y)) (cadar y))
        ('t (assoc x (cdr y))))
)

(assoc 'b  (('a (12 13 14) ('b 28))))

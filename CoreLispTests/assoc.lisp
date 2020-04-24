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

(setq associations
    '(
        (a (1 2)) 
        (b (3 4 5))
    )
) 
(print associations)


(print "assoc 'a")

(setq resa
    (assoc 
        'a  
        associations
    )
)

(print "assoc 'b")
(setq resb
    (assoc 
        'b  
        associations
    )
)
resb
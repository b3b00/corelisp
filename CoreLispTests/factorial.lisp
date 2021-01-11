(defun factorial (x) 
   (cond
            ((eq x 0) 1) 
            ('t (* x (factorial (- x 1))))
   )
) 

(factorial 10)
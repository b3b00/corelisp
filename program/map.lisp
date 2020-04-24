
(defun map (f l) 

	(debug "map" l
	    (cond
	        
            ((eq l ()) (debug "empty list" l ()))

            ('t (debug 
            		"not empty list" 
            		l
            		(cons ( f (car l)) 
            			  ( map f (cdr l) )
    			  	)
            	)
        	)
		)
	)
)

(defun plusone (x) (+ x 1))

(map plusone (1 2 3))
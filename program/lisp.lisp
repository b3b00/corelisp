

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
(print "=>" resa)

(print "assoc 'b")
(setq resb
    (assoc 
        'b  
        associations
    )
)
(print "=>" resb)




(defun cadr (x) (car (cdr x))
)

(defun caddr (x) (car (cdr (cdr x)))
)


(defun and (x y)
(cond (x (cond (y 't)
('t '())))
('t '())))

(defun append (x y)
	(cond 
		((null x) y)
		('t (cons (car x) (append (cdr x) y)))
		)
)


(defun list (x y z) 
	(cons x (cons y (cons z ())))
)



(defun pair (x y)
	(cond 
	    ((and (null x) (null y)) '())
		((and (not (atom x)) (not (atom y))) (cons (list (car x) (car y) ()) (pair (cdr x) (cdr y))))
    )
)



(defun evalAtom (exp env)
	(assoc exp env)
)

(defun evalDefun (exp env)
	(eval (cons (caddar exp) (cdr exp)) (cons (list (cadar exp) (car exp)) env))
)

(defun evalLambda (exp env)
	(eval (caddar exp) (append (pair (cadar exp) (listeval (cdr exp) env)) env))
)

(defun evalApply (exp env)
	
		(debug "debug apply" (car exp)
				(cond 
					((eq 'quote (car exp)) 
						(cadr exp))
					((eq 'atom (car exp)) 
						(atom (eval (cadr exp) env)))
					((eq 'eq (car exp))
						(eq (eval (cadr exp) env) (eval (caddr exp) env)))
					((eq 'car (car exp)) 
						(debug "debug evalcar" (car (eval (cadr exp) env))))
					((eq 'cdr (car exp)) 
						(cdr (eval (cadr exp) env)))
					((eq 'cons (car exp))
						(cons (eval (cadr exp) env) (eval (caddr exp) env)))
					((eq 'cond (car exp)) 
						(condeval (cdr exp) env))
					('t (eval (cons (assoc (car exp) env) (cdr exp)) env)))
		)
	
) 

(defun eval (exp env)
	(debug "eval"
		(cond
			((atom exp) 
				(debug "atom" exp
				(evalAtom exp env)
				)
			)
			((atom (car exp))
				(debug "apply" exp
				(evalApply exp env)
				)
			)
			((eq (caar exp) 'defun)
				(debug "defun" exp
				(evalDefun exp env)
				)
			)
			((eq (caar exp) 'lambda)
				(debug "lambda" exp
				(evalLambda exp env)
				)
			)
		)
	)
)



(setq env (pair ('a 'b) (12 28))) 
(setq evA
(eval '(car '(1 2 5 9)) env )
)
(print "eva" evA)


(setq envenv '((a (12 13 14) (b 28))))


 (setq evB
	(eval '(cdr a) envenv)
)
(print "evB" evB)






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
		((and (not (atom x)) (not (atom y))) (cons (list (car x) (car y)) (pair (cdr x) (cdr y))))
    )
)

(defun caar 
   (x) (car (car x))
) 

(defun cadar 
   (x) (car (cdr (car x)))
) 


(defun assoc (x y)
    (cond 
    	((null y) 'nil)
        ((eq x (caar y)) (cadar y))
        ('t (assoc x (cdr y))))
)


(defun null (x)
	(eq x '())
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
(
(cond 
				((eq 'quote (car exp)) 
					(cadr exp))
				((eq 'atom (car exp)) 
					(atom (eval (cadr exp) env)))
				((eq 'eq (car exp))
					(eq (eval (cadr exp) env) (eval (caddr exp) env)))
				((eq 'car (car exp)) 
					(car (eval (cadr exp) env)))
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
	(cond
		((atom exp) 
			(evalAtom exp env)
		)
		((atom (car exp))
			(evalApply exp env)
		)
		((eq (caar exp) 'defun)
			(evalDefun exp env)
		)
		((eq (caar exp) 'lambda)
			(evalLambda exp env)
		)
	)
)


#|
(setq evA
(eval '(car ("a" "b" "c")) (pair('t 'h 'h 'l) ('j 'p 'y 'o)))
)
(print evA)
(setq env (pair ('a 'b) (12 28))) 
|#
(setq envenv '((a (12 13 14) (b 28))))

#| (setq evB
	(eval '(cdr b) envenv)
)
(print evB)
evB |#

(assoc 'b  ((a (12 13 14) ('b 28))))
(print b)



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
        ((eq x (caar y)) (cadar y))
        ('t (assoc x (cdr y))))
)

(setq asso '((a b) (c d) (e f)))
(assoc 'a asso)

(defun null (x)
	(eq x '())
)



(defun eval (exp env)
(cond
((atom exp) (assoc exp env))
((atom (car exp))
(cond ((eq 'quote (car exp)) (cadr exp))
((eq 'atom (car exp)) (atom (eval (cadr exp) env)))
((eq 'eq (car exp))
(eq (eval (cadr exp) env) (eval (caddr exp) env)))
((eq 'car (car exp)) (car (eval (cadr exp) env)))
((eq 'cdr (car exp)) (cdr (eval (cadr exp) env)))
((eq 'cons (car exp))
(cons (eval (cadr exp) env) (eval (caddr exp) env)))
((eq 'cond (car exp)) (condeval (cdr exp) env))
('t (eval (cons (assoc (car exp) env) (cdr exp)) env))))
((eq (caar exp) 'label)
(eval (cons (caddar exp) (cdr exp))
(cons (list (cadar exp) (car exp)) env)))
((eq (caar exp) 'lambda)
(eval (caddar exp)
(append (pair (cadar exp) (listeval (cdr exp) env)) env)))
))

(defun listeval (args env)
	(cond 
		((null args) '())
		('t (cons 
			(eval (car args) env)
			(listeval (cdr args) env)))
	)
)

(eval '(car ("a" "b" "c")))

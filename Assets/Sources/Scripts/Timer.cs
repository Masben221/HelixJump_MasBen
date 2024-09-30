using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Cлайдер таймера, если null то таймер работет без него
	public  Slider 	SliderTimer;
	
	// Время работы таймера
	public	float	TimeTimer; 
	// Работет ли таймер
	public	bool	Enabled; 
	// Повтаряющийся таймер
	public	bool	Loop; 
	
	// Время таймера
	private float	CurrentTime; 

	// Шаблон события конца таймера
	public delegate void TimerEvent(); 
	public event	TimerEvent onEnd;


	// Шаблон события работы таймера
	public delegate void TimerUdate(float time); 
	public event	TimerUdate onUpdate;


	// Статический метод создающий таймер. Пример вызова Timer.CreateTimer(null, 5, false, SpawnWave); Возвращает созданные таймер
	public static Timer CreateTimer(Slider slider_timer, float time, bool loop, TimerEvent onEndEvent)
	{
		// Создаем игровой объект и добавляем к нему таймер
		GameObject TimerHandel = new GameObject();
		Timer timer = TimerHandel.AddComponent<Timer> ();
		// Запускаем таймер
		timer.StartTimer(slider_timer, time, loop);
		timer.onEnd += onEndEvent;
		return timer;

	}

	// Статический метод создающий таймер, с более короткими параметрами, для внутренних изменеринй, которыене не показываються на экране
	public static Timer CreateTimer(float time, TimerEvent onEndEvent)
	{
		// Создаем игровой объект и добавляем к нему таймер
		GameObject TimerHandel = new GameObject();
		Timer timer = TimerHandel.AddComponent<Timer> ();
		// Запускаем таймер
		timer.StartTimer(null, time, false);
		timer.onEnd += onEndEvent;
		return timer;

	}


	// Закончить действие таймера, и выполнить событие которое назначенное, если он есть
	public	void Complete()
	{
		Enabled = false;
		if(onEnd != null)
		{
			onEnd();
			Destroy(this.gameObject);
		}								
	}

	// Удаляет таймер без события
	public void CompleteNotEvent()
	{
		Enabled = false;
		Destroy(this.gameObject);
	}

	// Метод возвращающий время
	public	float GetTime()
	{
		return this.CurrentTime;
	}

	// Не уверен что нужно оставлять этот метод
	public  void  AddTime(float time)
	{
		this.CurrentTime -= time;
	}
	
	private void StartTimer(Slider slider_timer, float time, bool loop)
    {
       if(slider_timer != null) // Только если указан слайдер, что позволяет перекидывать таймер на слайдер. Если не укаан, таймер работает в обычно режиме
		{
			this.SliderTimer = slider_timer;
			this.SliderTimer.maxValue = time;
		}
		this.TimeTimer = time;
        this.Loop = loop;
        this.CurrentTime = 0f;
        this.Enabled = true;
    }
    

	// Update is called once per frame
	void Update () 
	{
		
		if(Enabled == true)
		{
			CurrentTime += Time.deltaTime;

			// Обнавляем значение слайдера, если он есть
            if(this.SliderTimer != null)
            {
                this.SliderTimer.value = this.SliderTimer.maxValue - this.CurrentTime;

            }
			
			// Вызываем события обновления, если оно есть
			if(onUpdate != null)
			{
				onUpdate(CurrentTime);
			}
			
			// Завершение таймера
			if(CurrentTime >= TimeTimer)
			{
				// Вызываем событие при завершении работы, если он есть
				if(onEnd != null)
				{
					onEnd();
					// Возможно как то оптимизировать!!!!!!!!
					if(Loop == false)
						Destroy(this.gameObject);
				}
				
				// Запускаем заново
				if(Loop == true)
					CurrentTime = 0;
					
				// Завершаем работу и удаляем 
				if(Loop == false)
				{
					Enabled = false;					
				}
			}
		
		}
		
	}
	
	
	
}

package com.crud.model;

public class Person {
	private int id;
	private String name;
    private String job;
    private boolean gender;
    private String birthDay;

	public Person() {
		super();
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getJob() {
		return job;
	}

	public void setJob(String job) {
		this.job = job;
	}

	public boolean isGender() {
		return gender;
	}

	public void setGender(boolean gender) {
		this.gender = gender;
	}

	public String getBirthDay() {
		return birthDay;
	}

	public void setBirthDay(String birthDay) {
		this.birthDay = birthDay;
	}

	public Person(int id, String name, String job, boolean gender, String birthDay) {
		this.id = id;
		this.name = name;
		this.job = job;
		this.gender = gender;
		this.birthDay = birthDay;
	}

}	
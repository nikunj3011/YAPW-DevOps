package com.crud.controller;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import com.crud.model.Person;
import com.crud.service.IPersonService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;


@Controller
public class PersonController {

	@Autowired
	private IPersonService personService;

	@RequestMapping(path = { "/index", "/" })
	public String listPerson(Model model, HttpServletRequest request) {
		
		HttpSession session = request.getSession();
		String listAll = (String) session.getAttribute("listAll");
		if (listAll == null) {
			session.setAttribute("listAll", "S");
			personService.getAll();
		}
		model.addAttribute("list", personService.listPerson());
		return "index";
	}

	@PostMapping(path = { "/search" })
	public String search(Model model, String name) {
		List<Person> list = personService.search(name);
		model.addAttribute("list", list);
		model.addAttribute("name", name);
		return "index";
	}

	@GetMapping(path = { "/inAdd" })
	public String inAdd(Model model) {
		return "form";
	}

	@PostMapping(path = { "/add" })
	public String add(Model model, Person p) {
		personService.add(p);
		return "redirect:/";
	}

	@GetMapping(path = "/delete/{id}")
	public String delete(Model model, @PathVariable(name = "id") int id) {
		personService.delete(id);
		return "redirect:/";
	}

	@GetMapping(path = "/iniEdit/{id}")
	public String iniEdit(Model model, @PathVariable(name = "name") String name) {
		List<Person> list = personService.search(name);
		Person obj = new Person();
		for (Person element : list) {
			obj = element;
			break;
		}
		model.addAttribute("person", obj);
		return "form";
	}

	@PostMapping(path = "/edit")
	public String edit(Model model,Person p) {
		personService.edit(p);
		return "redirect:/";
	}
}

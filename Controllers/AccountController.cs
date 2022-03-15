﻿using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers {
    public class AccountController : Controller {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult SignUp() {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel) {
            if(ModelState.IsValid) {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if(!result.Succeeded) {
                    foreach(var errorMessage in result.Errors) {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
            }
            return RedirectToAction(nameof(SignUp));
        }

        [Route("login")]
        public IActionResult LogIn() {
            return View();
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signInModel) {
            if(ModelState.IsValid) {
                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong Username/Password");
            }
            return View(signInModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout() {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

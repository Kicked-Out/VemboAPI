const navToggle = document.querySelector('.primary-nav__toggle');
const navList = document.querySelector('.primary-nav__list');

const updateNavState = (expanded) => {
  navToggle.setAttribute('aria-expanded', String(expanded));
  navList.dataset.open = expanded ? 'true' : 'false';
};

if (navToggle && navList) {
  navToggle.addEventListener('click', () => {
    const isExpanded = navToggle.getAttribute('aria-expanded') === 'true';
    updateNavState(!isExpanded);
  });

  navList.addEventListener('click', (event) => {
    if (event.target instanceof HTMLAnchorElement) {
      updateNavState(false);
    }
  });

  window.addEventListener('resize', () => {
    if (window.matchMedia('(min-width: 861px)').matches) {
      updateNavState(false);
    }
  });
}
